using System;
using System.Drawing;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Control
{
    /// <summary>
    /// A custom StatusStrip control
    /// which fix the problem that the spring item might push its right items out of view if too large.
    /// </summary>
    public class MyStatusStrip : RadStatusStrip
    {
        public MyStatusStrip()
        {
            this.ThemeClassName = "Telerik.WinControls.UI.RadStatusStrip";
        }

        protected override void OnLoad(Size desiredSize)
        {
            base.OnLoad(desiredSize);

            if (IsDesignMode)
            {
                return;
            }

            foreach (var item in this.Items)
            {
                if (!this.GetSpring(item))
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            this.SizeChanged += StatusStrip_SizeChanged;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bounds")
            {
                RadItem item = (RadItem)sender;
                if (!this.GetSpring(item))
                {
                    SetWidthForSpringItem();
                }
            }
        }

        private void StatusStrip_SizeChanged(object sender, EventArgs e)
        {
            if (!this.IsLoaded)
            {
                return;
            }
            SetWidthForSpringItem();
        }

        private int GetRemainingWidthForSpringItem()
        {
            int springItemWidth = this.Size.Width;
            foreach (var item in this.Items)
            {
                if (!this.GetSpring(item))
                {
                    springItemWidth = springItemWidth - item.Size.Width - item.Margin.Left - item.Margin.Right;
                }
            }
            return Math.Max(springItemWidth - 30, 0);
        }

        private void SetWidthForSpringItem()
        {
            foreach (var item in this.Items)
            {
                if (this.GetSpring(item))
                {
                    item.AutoSize = false;
                    var width = GetRemainingWidthForSpringItem();
                    item.SetBounds(item.Bounds.X, item.Bounds.Y, width - item.Margin.Left - item.Margin.Right, item.Bounds.Height);
                    break;
                }
            }
        }
    }
}