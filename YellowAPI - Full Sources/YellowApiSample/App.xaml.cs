/*
Copyright © 2011, Yellow Pages Group Co.  All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1)	Redistributions of source code must retain a complete copy of this notice, including the copyright notice, this list of conditions and the following disclaimer; and
2)	Neither the name of the Yellow Pages Group Co., nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT OWNER AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using YellowApiSample.ViewModels;
using YellowPages.YellowApi;

namespace YellowApiSample
{
  public partial class App : Application
  {
    /// <summary>
    /// Provides easy access to the root frame of the Phone Application.
    /// </summary>
    /// <returns>The root frame of the Phone Application.</returns>
    public PhoneApplicationFrame RootFrame { get; private set; }

    /// <summary>
    /// Constructor for the Application object.
    /// </summary>
    public App()
    {
      // Global handler for uncaught exceptions. 
      UnhandledException += Application_UnhandledException;

      /*
      // Show graphics profiling information while debugging.
      if( System.Diagnostics.Debugger.IsAttached )
      {
        // Display the current frame rate counters.
        Application.Current.Host.Settings.EnableFrameRateCounter = true;

        // Show the areas of the app that are being redrawn in each frame.
        //Application.Current.Host.Settings.EnableRedrawRegions = true;

        // Enable non-production analysis visualization mode, 
        // which shows areas of a page that are being GPU accelerated with a colored overlay.
        //Application.Current.Host.Settings.EnableCacheVisualization = true;
      }
      */

      // Standard Silverlight initialization
      InitializeComponent();

      // Phone-specific initialization
      InitializePhoneApplication();

      #error You must enter your YellowAPI application key below
      // In order to use YellowAPI, you must apply for an application key by visiting
      // http://www.yellowapi.com/member/register and create an account. Once your account 
      // is created and validated, you can visit http://www.yellowapi.com/member/my-account
      // to view your sandbox and production keys. Simply copy the appropriate key below
      // to test this sample.
      YellowApiHelper.ApplicationKey = "YOUR YELLOWAPI APPLICATION KEY";

      YellowApiHelper.UseSandBox = true;
      YellowApiHelper.UserUniqueID = "YellowAPI Sample";

      MainViewModel.Current = this.Resources[ "MainViewModel" ] as MainViewModel;
    }

    // Code to execute when the application is launching (eg, from Start)
    // This code will not execute when the application is reactivated
    private void Application_Launching( object sender, LaunchingEventArgs e )
    {
    }

    // Code to execute when the application is activated (brought to foreground)
    // This code will not execute when the application is first launched
    private void Application_Activated( object sender, ActivatedEventArgs e )
    {
      MainViewModel.Current.LoadState();
    }

    // Code to execute when the application is deactivated (sent to background)
    // This code will not execute when the application is closing
    private void Application_Deactivated( object sender, DeactivatedEventArgs e )
    {
      MainViewModel.Current.SaveState();
    }

    // Code to execute when the application is closing (eg, user hit Back)
    // This code will not execute when the application is deactivated
    private void Application_Closing( object sender, ClosingEventArgs e )
    {
    }

    // Code to execute if a navigation fails
    private void RootFrame_NavigationFailed( object sender, NavigationFailedEventArgs e )
    {
      if( System.Diagnostics.Debugger.IsAttached )
      {
        // A navigation has failed; break into the debugger
        System.Diagnostics.Debugger.Break();
      }
    }

    // Code to execute on Unhandled Exceptions
    private void Application_UnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
    {
      if( System.Diagnostics.Debugger.IsAttached )
      {
        // An unhandled exception has occurred; break into the debugger
        System.Diagnostics.Debugger.Break();
      }
    }

    #region Phone application initialization

    // Avoid double-initialization
    private bool phoneApplicationInitialized = false;

    // Do not add any additional code to this method
    private void InitializePhoneApplication()
    {
      if( phoneApplicationInitialized )
        return;

      // Create the frame but don't set it as RootVisual yet; this allows the splash
      // screen to remain active until the application is ready to render.
      RootFrame = new PhoneApplicationFrame();
      RootFrame.Navigated += CompleteInitializePhoneApplication;

      // Handle navigation failures
      RootFrame.NavigationFailed += RootFrame_NavigationFailed;

      // Ensure we don't initialize again
      phoneApplicationInitialized = true;
    }

    // Do not add any additional code to this method
    private void CompleteInitializePhoneApplication( object sender, NavigationEventArgs e )
    {
      // Set the root visual to allow the application to render
      if( RootVisual != RootFrame )
        RootVisual = RootFrame;

      // Remove this handler since it is no longer needed
      RootFrame.Navigated -= CompleteInitializePhoneApplication;
    }

    #endregion
  }
}