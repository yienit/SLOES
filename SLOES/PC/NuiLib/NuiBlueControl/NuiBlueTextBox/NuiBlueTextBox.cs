﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NuiLib.NuiBlueControl
{
    /// <summary>
    /// 腾讯QQ蓝色文本框控件
    /// </summary>
    public class NuiBlueTextBox : TextBox
    {
        #region Field

        private NuiBlueControlState state = NuiBlueControlState.Normal;
        private Font defaultFont = new Font("微软雅黑", 9);

        //当Text属性为空时编辑框内出现的提示文本
        private string emptyTextTip;
        private Color emptyTextTipColor = Color.DarkGray;

        #endregion

        #region Constructor

        public NuiBlueTextBox()
        {
            SetStyles();
            this.Font = defaultFont;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion

        #region Properites

        [Browsable(true)]
        [Description("当Text属性为空时编辑框内出现的提示文本")]
        public String EmptyTextTip
        {
            get { return emptyTextTip; }
            set
            {
                if (emptyTextTip != value)
                {
                    emptyTextTip = value;
                    base.Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Description("获取或设置EmptyTextTip的颜色")]
        public Color EmptyTextTipColor
        {
            get { return emptyTextTipColor; }
            set
            {
                if (emptyTextTipColor != value)
                {
                    emptyTextTipColor = value;
                    base.Invalidate();
                }
            }
        }

        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            state = NuiBlueControlState.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (state == NuiBlueControlState.Hover && Focused)
            {
                state = NuiBlueControlState.Focus;
            }
            else if (state == NuiBlueControlState.Focus)
            {
                state = NuiBlueControlState.Focus;
            }
            else
            {
                state = NuiBlueControlState.Normal;
            }
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                state = NuiBlueControlState.Hover;
            }
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(mevent.Location))
                {
                    state = NuiBlueControlState.Hover;
                }
                else
                {
                    state = NuiBlueControlState.Focus;
                }
            }
            this.Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            state = NuiBlueControlState.Focus;
            this.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            state = NuiBlueControlState.Normal;
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                state = NuiBlueControlState.Normal;
            }
            else
            {
                state = NuiBlueControlState.Disabled;
            }
            this.Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            //TextBox是由系统进程绘制，重载OnPaint方法将不起作用
            base.WndProc(ref m);
            if (m.Msg == Win32.WM_PAINT || m.Msg == Win32.WM_CTLCOLOREDIT)
            {
                WmPaint(ref m);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (defaultFont != null)
                {
                    defaultFont.Dispose();
                }
            }

            defaultFont = null;
            base.Dispose(disposing);
        }

        #endregion

        #region Private

        private void SetStyles()
        {
            // TextBox由系统绘制，不能设置 ControlStyles.UserPaint样式
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void WmPaint(ref Message m)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!Enabled)
            {
                state = NuiBlueControlState.Disabled;
            }

            switch (state)
            {
                case NuiBlueControlState.Normal:
                    DrawNormalTextBox(g);
                    break;
                case NuiBlueControlState.Hover:
                    DrawHoverTextBox(g);
                    break;
                case NuiBlueControlState.Focus:
                    DrawFocusTextBox(g);
                    break;
                case NuiBlueControlState.Disabled:
                    DrawDisabledTextBox(g);
                    break;
                default:
                    break;
            }

            if (Text.Length == 0 && !string.IsNullOrEmpty(EmptyTextTip) && !Focused)
            {
                TextRenderer.DrawText(g, EmptyTextTip, Font, ClientRectangle, EmptyTextTipColor, GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
            }
        }

        private void DrawNormalTextBox(Graphics g)
        {
            using (Pen borderPen = new Pen(Color.LightGray))
            {
                g.DrawRectangle(
                    borderPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        private void DrawHoverTextBox(Graphics g)
        {
            using (Pen highLightPen = new Pen(NuiBlueColorTable.QQHoverColor))
            {
                Rectangle drawRect = new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1);

                g.DrawRectangle(highLightPen, drawRect);

                //InnerRect
                drawRect.Inflate(-1, -1);
                highLightPen.Color = NuiBlueColorTable.QQHoverInnerColor;
                g.DrawRectangle(highLightPen, drawRect);
            }
        }

        private void DrawFocusTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(NuiBlueColorTable.QQHoverInnerColor))
            {
                g.DrawRectangle(
                    focusedBorderPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        private void DrawDisabledTextBox(Graphics g)
        {
            using (Pen disabledPen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(disabledPen,
                    new Rectangle(
                        ClientRectangle.X,
                        ClientRectangle.Y,
                        ClientRectangle.Width - 1,
                        ClientRectangle.Height - 1));
            }
        }

        private static TextFormatFlags GetTextFormatFlags(HorizontalAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak |
                TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }

        #endregion
    }
}
