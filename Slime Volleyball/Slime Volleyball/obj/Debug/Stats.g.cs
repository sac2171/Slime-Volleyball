﻿#pragma checksum "M:\SkyDrive\GitHub\Slime-Volleyball\Slime Volleyball\Slime Volleyball\Stats.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E9222E19AD0E31682E4BAD8CB7FD1CE9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
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


namespace Slime_Volleyball {
    
    
    public partial class Stats : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock victories_tb;
        
        internal System.Windows.Controls.TextBlock games_tb;
        
        internal System.Windows.Controls.TextBlock sent_tb;
        
        internal System.Windows.Controls.TextBlock received_tb;
        
        internal System.Windows.Controls.TextBlock data_tb;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Slime%20Volleyball;component/Stats.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.victories_tb = ((System.Windows.Controls.TextBlock)(this.FindName("victories_tb")));
            this.games_tb = ((System.Windows.Controls.TextBlock)(this.FindName("games_tb")));
            this.sent_tb = ((System.Windows.Controls.TextBlock)(this.FindName("sent_tb")));
            this.received_tb = ((System.Windows.Controls.TextBlock)(this.FindName("received_tb")));
            this.data_tb = ((System.Windows.Controls.TextBlock)(this.FindName("data_tb")));
        }
    }
}

