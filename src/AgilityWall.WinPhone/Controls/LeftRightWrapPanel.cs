using System;
using System.Windows;
using System.Windows.Controls;

namespace AgilityWall.WinPhone.Controls
{
    public class LeftRightWrapPanel : Panel
    {
        bool _wrap;

        protected override Size MeasureOverride(Size constraint)
        {
            if (Children.Count < 2)
            {
                return base.MeasureOverride(constraint);
            }
            var finalSize = new Size();

            Children[0].Measure(constraint);
            Children[1].Measure(constraint);

            if (Children[0].DesiredSize.Width + Children[1].DesiredSize.Width <= constraint.Width)
            {
                _wrap = false;
                finalSize.Width = Children[0].DesiredSize.Width + Children[1].DesiredSize.Width;
                finalSize.Height = Math.Max(Children[0].DesiredSize.Height, Children[1].DesiredSize.Height);
            }
            else
            {
                _wrap = true;
                finalSize.Height = Children[0].DesiredSize.Height + Children[1].DesiredSize.Height;
                finalSize.Width = Math.Max(Children[0].DesiredSize.Width, Children[1].DesiredSize.Width);
            }
            return finalSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_wrap)
            {
                Children[0].Arrange(new Rect(0, 0, finalSize.Width, Children[0].DesiredSize.Height));
                Children[1].Arrange(new Rect(0, Children[0].DesiredSize.Height, finalSize.Width, finalSize.Height - Children[0].DesiredSize.Height));
            }
            else
            {
                Children[0].Arrange(new Rect(0, 0, finalSize.Width, Children[0].DesiredSize.Height));
                Children[1].Arrange(new Rect(finalSize.Width - Children[1].DesiredSize.Width, 0, Children[1].DesiredSize.Width, finalSize.Height));

            }
            return base.ArrangeOverride(finalSize);
        }
    }
}