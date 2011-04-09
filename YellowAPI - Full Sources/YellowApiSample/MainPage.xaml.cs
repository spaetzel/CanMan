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
  public partial class MainPage : PhoneApplicationPage
  {
    private MainViewModel _viewModel;
 
    public MainPage()
    {
      InitializeComponent();

      _viewModel = MainViewModel.Current;

      this.Loaded += new RoutedEventHandler( MainPage_Loaded );
    }

    protected override void OnNavigatedFrom( System.Windows.Navigation.NavigationEventArgs e )
    {
      base.OnNavigatedFrom( e );

      _viewModel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler( MainViewModel_PropertyChanged );
    }

    protected override void OnNavigatedTo( System.Windows.Navigation.NavigationEventArgs e )
    {
      base.OnNavigatedTo( e );

      _viewModel.SearchResults = null;
      _viewModel.SelectedListing = null;

      _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler( MainViewModel_PropertyChanged );
    }

    private void MainViewModel_PropertyChanged( object sender, System.ComponentModel.PropertyChangedEventArgs e )
    {
      if( e.PropertyName == "SearchResults" )
      {
        if( _viewModel.SearchResults != null )
        {
          this.Dispatcher.BeginInvoke( () =>
            {
              if( _viewModel.SearchResults.Error is WebException )
              {
                MessageBox.Show(
                  "No results could be found for the specified search terms.",
                  "NOT FOUND",
                  MessageBoxButton.OK );
              }
              else if( _viewModel.SearchResults.Error != null )
              {
                MessageBox.Show(
                  "An error occurred trying to communicate with the YellowPages servers. Make sure your device can connect to the Internet.",
                  "ERROR",
                  MessageBoxButton.OK );
              }
              else if( _viewModel.SearchResults.Results != null )
              {
                this.NavigationService.Navigate( new Uri( "/SearchResultsPage.xaml", UriKind.Relative ) );
              }
            } );
        }
      }
    }

    private void MainPage_Loaded( object sender, RoutedEventArgs e )
    {
      this.Loaded -= new RoutedEventHandler( MainPage_Loaded );

      this.DataContext = _viewModel;
    }

    private void FindButton_Click( object sender, RoutedEventArgs e )
    {
      // We could support "current location" from here.
      if( !string.IsNullOrEmpty( WhatTextBox.Text ) && !string.IsNullOrEmpty( WhereTextBox.Text ) )
      {
        try
        {
          _viewModel.Search( WhatTextBox.Text, WhereTextBox.Text );
        }
        catch
        {
          MessageBox.Show(
            "An unexpected error occurred. Make sure your device can connect to the Internet.",
            "ERROR",
            MessageBoxButton.OK );
        }
      }
    }
  }
}