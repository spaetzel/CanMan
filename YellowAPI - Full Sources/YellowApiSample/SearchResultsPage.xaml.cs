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
  public partial class SearchResultsPage : PhoneApplicationPage
  {
    private SearchResultsViewModel _viewModel;

    public SearchResultsPage()
    {
      InitializeComponent();

      _viewModel = MainViewModel.Current.SearchResults;
      System.Diagnostics.Debug.Assert( _viewModel != null, "Creating SearchResultsPage without results. Check state persistence?" );

      this.Loaded += new RoutedEventHandler( SearchResultsPage_Loaded );
    }

    protected override void OnNavigatedFrom( System.Windows.Navigation.NavigationEventArgs e )
    {
      base.OnNavigatedFrom( e );

      MainViewModel.Current.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler( MainViewModel_PropertyChanged );
    }

    protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e )
    {
      // Make sure the MainViewModel does not have any selected listing.
      MainViewModel.Current.SelectedListing = null;

      base.OnNavigatedTo( e );

      MainViewModel.Current.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler( MainViewModel_PropertyChanged );
    }

    private void SearchResultsPage_Loaded( object sender, RoutedEventArgs e )
    {
      this.Loaded -= new RoutedEventHandler( SearchResultsPage_Loaded );

      this.DataContext = _viewModel;
    }

    private void MainViewModel_PropertyChanged( object sender, System.ComponentModel.PropertyChangedEventArgs e )
    {
      if( e.PropertyName == "SelectedListing" )
      {
        if( MainViewModel.Current.SelectedListing != null )
        {
          this.NavigationService.Navigate( new Uri( "/ListingPage.xaml", UriKind.Relative ) );
        }
      }
    }

    private void ResultsListBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
    {
      if( e.AddedItems.Count == 1 )
      {
        MainViewModel.Current.SelectedListing = e.AddedItems[ 0 ] as BusinessListingViewModel;

        // Remove selection to allow re-selecting the same item.
        ResultsListBox.SelectedItem = null;
      }
    }
  }
}