using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Coursework1.Core;

namespace Coursework1
{
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member

        private Window _Window;

        private int _OuterMarginSize = 10;

        private int _WindowRadius = 10;

        private WindowDockPosition _DockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        public int WindowMinimumWidth { get; set; } = 800;

        public int WindowMinimumHeight { get; set; } = 600;

        public bool Borderless => _Window.WindowState == WindowState.Maximized || _DockPosition != WindowDockPosition.Undocked;

        public int ResizeBorder => Borderless ? 0 : 6;

        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public int OuterMarginSize
        {
            get => _Window.WindowState == WindowState.Maximized ? 0 : _OuterMarginSize;
            set => _OuterMarginSize = value;
        }

        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        public int WindowRadius
        {
            get => _Window.WindowState == WindowState.Maximized ? 0 : _OuterMarginSize;
            set => _OuterMarginSize = value;
        }

        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        #endregion

        #region Default Constructor

        public WindowViewModel(Window window)
        {
            _Window = window;

            _Window.StateChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            MinimizeCommand = new RelayCommand(() => _Window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _Window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _Window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_Window, GetMousePosition()));

            var resizer = new WindowResizer(_Window);
        }

        #endregion

        #region Helpers

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        private static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        #endregion
    }
}