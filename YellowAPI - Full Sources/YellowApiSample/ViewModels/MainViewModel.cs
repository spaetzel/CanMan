/*
Copyright © 2011, Yellow Pages Group Co.  All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1)	Redistributions of source code must retain a complete copy of this notice, including the copyright notice, this list of conditions and the following disclaimer; and
2)	Neither the name of the Yellow Pages Group Co., nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT OWNER AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using YellowPages.YellowApi;
using YellowPages.YellowApi.Business;
using Microsoft.Phone.Shell;
using System.Linq;

namespace YellowApiSample.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    public MainViewModel()
    {
      // Do not set MainViewModel.Current here. There can be more than one instance in design-time.
      // The App instance is responsible for setting the current MainViewModel from its resources.

      if( this.IsInDesignTime )
      {
        _searchResults = new SearchResultsViewModel();
        _selectedListing = new BusinessListingViewModel();
      }
    }

    #region Current STATIC PROPERTY

    public static MainViewModel Current { get; internal set; }

    #endregion

    #region Language PROPERTY

    private YellowApiLanguage? _language;

    public YellowApiLanguage Language
    {
      get
      {
        if( !_language.HasValue )
        {
          _language = ( System.Threading.Thread.CurrentThread.CurrentUICulture.Name.StartsWith( "fr", StringComparison.InvariantCultureIgnoreCase ) )
            ? YellowApiLanguage.French : YellowApiLanguage.English;
        }

        return _language.Value;
      }
    }

    #endregion

    #region SearchResults PROPERTY

    private SearchResultsViewModel _searchResults;

    public SearchResultsViewModel SearchResults
    {
      get { return _searchResults; }
      set
      {
        if( value != _searchResults )
        {
          _searchResults = value;
          this.NotifyPropertyChanged( "SearchResults" );
        }
      }
    }

    #endregion

    #region Searching PROPERTY

    private bool _searching;

    public bool Searching
    {
      get { return _searching; }
      set
      {
        if( value != _searching )
        {
          _searching = value;
          this.NotifyPropertyChanged( "Searching" );
        }
      }
    }

    #endregion

    #region SelectedListing PROPERTY

    private BusinessListingViewModel _selectedListing;

    public BusinessListingViewModel SelectedListing
    {
      get { return _selectedListing; }
      set
      {
        if( value != _selectedListing )
        {
          _selectedListing = value;
          this.NotifyPropertyChanged( "SelectedListing" );
        }
      }
    }

    #endregion

    public void Search( string what, string where )
    {
      this.Searching = true;

      try
      {
        YellowApiHelper.FindBusinessAsync(
          what, where, 1, this.Language, YellowApiFlags.None,
          new YellowApiCallback<SearchResults>( this.SearchCompleted ),
          null );
      }
      catch
      {
        this.Searching = false;
        throw;
      }
    }

    public void Search( string what, GeoCoordinate where )
    {
      this.Searching = true;

      try
      {
        YellowApiHelper.FindBusinessAsync(
          what, where, 1, YellowApiLanguage.English, YellowApiFlags.None,
          new YellowApiCallback<SearchResults>( this.SearchCompleted ),
          null );
      }
      catch
      {
        this.Searching = false;
        throw;
      }
    }

    public override void LoadState()
    {
      object value = null;

      if( PhoneApplicationService.Current.State.TryGetValue( "SearchResults", out value ) && ( bool )value )
      {
        _searchResults = new SearchResultsViewModel();
        _searchResults.LoadState();
      }

      if( PhoneApplicationService.Current.State.TryGetValue( "SelectedListing", out value ) )
      {
        string id = ( string )value;

        _selectedListing = _searchResults.Listings.FirstOrDefault(
          ( l ) =>
          {
            return ( l.Id == id );
          } );

        if( _selectedListing != null )
        {
          // We give that listing the chance to load its details, if any.
          _selectedListing.LoadState();
        }
      }
    }

    public override void SaveState()
    {
      if( _searchResults != null )
      {
        _searchResults.SaveState();
        PhoneApplicationService.Current.State[ "SearchResults" ] = true;
      }
      else
      {
        // This would not be necessary, since the state is cleared after activation/launch, but let's be safe.
        PhoneApplicationService.Current.State[ "SearchResults" ] = false;
      }

      if( _selectedListing != null )
      {
        // We save the ID, so we can find it back after loading _searchResults.
        PhoneApplicationService.Current.State[ "SelectedListing" ] = _selectedListing.Id;

        // We tell that ListingViewModel to save its state. What it actually does is only save
        // its details, if any.
        _selectedListing.SaveState();
      }
    }

    private void SearchCompleted( SearchResults results, Exception except )
    {
      this.Dispatcher.BeginInvoke( () =>
        {
          this.SearchResults = new SearchResultsViewModel()
            {
              Error = except,
              Results = results
            };

          this.Searching = false;
        } );
    }
  }
}
