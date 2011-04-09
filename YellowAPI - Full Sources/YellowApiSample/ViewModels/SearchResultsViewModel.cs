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
using YellowPages.YellowApi.Business;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace YellowApiSample.ViewModels
{
  public class SearchResultsViewModel : BaseViewModel
  {
    public SearchResultsViewModel()
    {
      if( this.IsInDesignTime )
      {
        this.AddResults( new SearchResults()
        {
          Listings = new Listing[]
          {
            new Listing()
            {
              Name = "Auberge Du Dragon Rouge",
              Address = new Address()
              {
                Street = "8870, rue Lajeunesse",
                City = "Montréal",
                Province = "QC",
                PostalCode = "H2M1R6"
              },
              Coordinates = new GeoCode()
              {
                Longitude = -73.4,
                Latitude = 45.6
              },
              Distance = 0.7,
              Content = new Content()
              {
                Video = new Content.ContentInfo() { Available = true },
                Url = new Content.ContentInfo() { Available = true }
              }
            },
            new Listing()
            {
              Name = "Auberge Du Dragon Rouge",
              Address = new Address()
              {
                Street = "8870, rue Lajeunesse",
                City = "Montréal",
                Province = "QC",
                PostalCode = "H2M1R6"
              },
              Coordinates = new GeoCode()
              {
                Longitude = -73.4,
                Latitude = 45.6
              },
              Distance = 0.7,
              Content = new Content()
              {
                Video = new Content.ContentInfo() { Available = true },
                Url = new Content.ContentInfo() { Available = true }
              }
            }
          }
        } );
      }
    }

    private Exception _error;

    public Exception Error
    {
      get { return _error; }
      set
      {
        if( value != _error )
        {
          _error = value;
          this.NotifyPropertyChanged( "Error" );
        }
      }
    }

    private ObservableCollection<BusinessListingViewModel> _listings = new ObservableCollection<BusinessListingViewModel>();

    public ObservableCollection<BusinessListingViewModel> Listings
    {
      get { return _listings; }
    }

    private List<SearchResults> _results = new List<SearchResults>();

    public SearchResults Results
    {
      get { return ( _results.Count == 0 ) ? null : _results[ 0 ]; }
      set
      {
        _results.Clear();

        if( value != null )
        {
          this.AddResults( value );
        }

        this.NotifyPropertyChanged( "Results" );
      }
    }

    public override void LoadState()
    {
      try
      {
        using( IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication() )
        {
          if( storage.FileExists( "SearchResults.xml" ) )
          {
            using( Stream stream = storage.OpenFile( "SearchResults.xml", FileMode.Open, FileAccess.Read ) )
            {
              XmlSerializer serializer = new XmlSerializer( typeof( List<SearchResults> ) );

              List<SearchResults> resultsList = ( List<SearchResults> )serializer.Deserialize( stream );

              foreach( SearchResults results in resultsList )
              {
                this.AddResults( results );
              }
            }
          }
        }
      }
      catch { }
    }

    public override void SaveState()
    {
      // We could have saved ListingViewModel instances directly, so we could keep DetailsResults we
      // already have. But this also causes loading to be longer. For this sample, we're saving SearchResults,
      // rebuilding our ListingViewModels on loading, dropping any details we could already have.
      if( _results != null )
      {
        try
        {
          using( IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication() )
          {
            using( Stream stream = storage.OpenFile( "SearchResults.xml", FileMode.Create, FileAccess.Write ) )
            {
              XmlSerializer serializer = new XmlSerializer( typeof( List<SearchResults> ) );
              serializer.Serialize( stream, _results );
            }
          }
        }
        catch { }
      }
    }

    private void AddResults( SearchResults results )
    {
      _results.Add( results );

      foreach( Listing listing in results.Listings )
      {
        _listings.Add( new BusinessListingViewModel() { Listing = listing } );
      }
    }
  }
}
