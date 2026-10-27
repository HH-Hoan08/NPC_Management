using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NPC.Server
{
    public class FormScaler
    {
        private Size originalFormSize;
        private readonly Dictionary<Control, (Rectangle Bounds, float FontSize)> originalControls
            = new Dictionary<Control, (Rectangle, float)>();

        /// <summary>
        /// Đăng ký Form cần scale. Gọi hàm này ở Constructor của Form (sau InitializeComponent).
        /// </summary>
        public static FormScaler EnableAutoScaling(Form form)
        {
            var scaler = new FormScaler();
            form.Load += (s, e) => scaler.Initialize(form);
            form.Resize += (s, e) => scaler.Scale(form);
            return scaler;
        }

        private void Initialize(Form form)
        {
            originalFormSize = form.ClientSize;
            originalControls.Clear();
            SaveOriginalControlBounds(form);
        }

        private void SaveOriginalControlBounds(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (!originalControls.ContainsKey(c))
                {
                    originalControls.Add(c, (new Rectangle(c.Location, c.Size), c.Font.Size));
                }
                if (c.HasChildren)
                {
                    SaveOriginalControlBounds(c);
                }
            }
        }

        private void Scale(Form form)
        {
            if (originalFormSize.Width == 0 || originalFormSize.Height == 0) return;

            float scaleX = (float)form.ClientSize.Width / originalFormSize.Width;
            float scaleY = (float)form.ClientSize.Height / originalFormSize.Height;
            float scaleFont = Math.Min(scaleX, scaleY);

            form.SuspendLayout();

            foreach (var kvp in originalControls)
            {
                Control c = kvp.Key;
                Rectangle origBounds = kvp.Value.Bounds;
                float origFontSize = kvp.Value.FontSize;

                int newX = (int)(origBounds.X * scaleX);
                int newY = (int)(origBounds.Y * scaleY);
                int newWidth = (int)(origBounds.Width * scaleX);
                int newHeight = (int)(origBounds.Height * scaleY);

                c.Location = new Point(newX, newY);
                c.Size = new Size(newWidth, newHeight);

                if (origFontSize > 0)
                {
                    c.Font = new Font(c.Font.FontFamily, Math.Max(1f, origFontSize * scaleFont), c.Font.Style);
                }
            }

            form.ResumeLayout();
        }
    }
}