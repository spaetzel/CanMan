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
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using YellowApiSample.ViewModels;

namespace YellowApiSample
{
  public partial class VideoPage : PhoneApplicationPage
  {
    private BusinessListingViewModel _viewModel;

    public VideoPage()
    {
      InitializeComponent();

      _viewModel = MainViewModel.Current.SelectedListing;
      System.Diagnostics.Debug.Assert( _viewModel != null, "Creating VideoPage without a selected listing. Check state persistence?" );

      this.Loaded += new RoutedEventHandler( VideoPage_Loaded );
    }

    private void VideoPage_Loaded( object sender, RoutedEventArgs e )
    {
      this.Loaded -= new RoutedEventHandler( VideoPage_Loaded );

      this.DataContext = _viewModel;
    }

    private void RewindButton_Click( object sender, RoutedEventArgs e )
    {
      try
      {
        VideoMediaElement.Position = TimeSpan.Zero;
      }
      catch { }
    }

    private void PlayButton_Click( object sender, RoutedEventArgs e )
    {
      try
      {
        VideoMediaElement.Play();
      }
      catch { }
    }

    private void PauseButton_Click( object sender, RoutedEventArgs e )
    {
      try
      {
        VideoMediaElement.Pause();
      }
      catch { }
    }

    private void VideoMediaElement_CurrentStateChanged( object sender, RoutedEventArgs e )
    {
      switch( VideoMediaElement.CurrentState )
      {
        case MediaElementState.Playing:
          VisualStateManager.GoToState( this, "PlayingState", true );
          break;

        case MediaElementState.Paused:
          VisualStateManager.GoToState( this, "PausedState", true );
          break;

        case MediaElementState.Stopped:
          VisualStateManager.GoToState( this, "StoppedState", true );
          break;
      }
    }

    private void VideoMediaElement_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
    {
      if( VideoMediaElement.CurrentState == MediaElementState.Playing )
      {
        VisualStateManager.GoToState( this, "PlayingTouchedState", true );
      }
    }
  }
}
