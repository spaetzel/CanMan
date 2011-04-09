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
using Microsoft.Phone.Tasks;

namespace YellowApiSample
{
  public partial class ListingPage : PhoneApplicationPage
  {
    private BusinessListingViewModel _viewModel;

    public ListingPage()
    {
      InitializeComponent();

      _viewModel = MainViewModel.Current.SelectedListing;
      System.Diagnostics.Debug.Assert( _viewModel != null, "Creating ListingPage without a selected listing. Check state persistence?" );

      this.Loaded += new RoutedEventHandler( ListingPage_Loaded );
    }

    private void ListingPage_Loaded( object sender, RoutedEventArgs e )
    {
      this.Loaded -= new RoutedEventHandler( ListingPage_Loaded );

      this.DataContext = _viewModel;

      if( _viewModel != null )
      {
        try
        {
          _viewModel.UpdateDetails();
        }
        catch { } // We don't display any error.
      }
    }

    private void PhoneButton_Click( object sender, RoutedEventArgs e )
    {
      try
      {
        if( !string.IsNullOrEmpty( _viewModel.Phone ) )
        {
          PhoneCallTask task = new PhoneCallTask()
            {
              DisplayName = _viewModel.Name,
              PhoneNumber = _viewModel.Phone
            };

          task.Show();
        }
      }
      catch { }
    }

    private void WebSiteButton_Click( object sender, RoutedEventArgs e )
    {
      try
      {
        if( !string.IsNullOrEmpty( _viewModel.WebUrl ) )
        {
          WebBrowserTask task = new WebBrowserTask()
            {
              URL = _viewModel.WebUrl
            };

          task.Show();
        }
      }
      catch { }
    }

    private void VideoButton_Click( object sender, RoutedEventArgs e )
    {
      NavigationService.Navigate( new Uri( "/VideoPage.xaml", UriKind.Relative ) );
    }
  }
}
