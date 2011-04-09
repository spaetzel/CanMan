﻿#pragma checksum "C:\Users\Jean-Luc David\Desktop\YellowAPI - Full Sources\YellowApiSample\VideoPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4253DF3CA058B1E0A785B0BF57551150"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace YellowApiSample {
    
    
    public partial class VideoPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup VideoStateGroup;
        
        internal System.Windows.VisualState StoppedState;
        
        internal System.Windows.VisualState PausedState;
        
        internal System.Windows.VisualState PlayingState;
        
        internal System.Windows.VisualState PlayingTouchedState;
        
        internal System.Windows.Controls.MediaElement VideoMediaElement;
        
        internal System.Windows.Controls.StackPanel stackPanel;
        
        internal System.Windows.Controls.Button RewindButton;
        
        internal System.Windows.Controls.Button PlayButton;
        
        internal System.Windows.Controls.Button PauseButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/YellowApiSample;component/VideoPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.VideoStateGroup = ((System.Windows.VisualStateGroup)(this.FindName("VideoStateGroup")));
            this.StoppedState = ((System.Windows.VisualState)(this.FindName("StoppedState")));
            this.PausedState = ((System.Windows.VisualState)(this.FindName("PausedState")));
            this.PlayingState = ((System.Windows.VisualState)(this.FindName("PlayingState")));
            this.PlayingTouchedState = ((System.Windows.VisualState)(this.FindName("PlayingTouchedState")));
            this.VideoMediaElement = ((System.Windows.Controls.MediaElement)(this.FindName("VideoMediaElement")));
            this.stackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel")));
            this.RewindButton = ((System.Windows.Controls.Button)(this.FindName("RewindButton")));
            this.PlayButton = ((System.Windows.Controls.Button)(this.FindName("PlayButton")));
            this.PauseButton = ((System.Windows.Controls.Button)(this.FindName("PauseButton")));
        }
    }
}
