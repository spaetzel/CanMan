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
using System.Text;
using System.Linq;
using YellowPages.YellowApi;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Phone.Shell;

namespace YellowApiSample.ViewModels
{
  public class BusinessListingViewModel : BaseViewModel
  {
    public BusinessListingViewModel()
    {
      if( this.IsInDesignTime )
      {
        _listing = new Listing()
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
              DisplayAd = new Content.ContentInfo() { Available = true },
              Logo = new Content.ContentInfo() { Available = true },
              Photo = new Content.ContentInfo() { Available = true },
              Video = new Content.ContentInfo() { Available = true },
              Url = new Content.ContentInfo() { Available = true }
            }
          };

        _details = new DetailsResults()
          {
            Name = _listing.Name,
            Address = _listing.Address,
            Coordinates = _listing.Coordinates,
            Phones = new Phone[]
              {
                new Phone()
                {
                  AreaCode = "514",
                  ExchangeNumber = "555",
                  ListingNumber = "1234",
                  DisplayNumber = "514-555-1234"
                }
              },
            MerchantUrl = "",
            Products = new Product[]
              {
                new Video() { Url = "" },
                new Photo() { Url = "" },
                new DisplayAd() { Url = "http://ci.yp.ca/13061832ab_f.jpg" },
                new WebAddress() { Url = "http://www.slimcode.com" }
              }                  
          };
      }
    }

    public string Address
    {
      get
      {
        if( _listing == null )
          return "";

        StringBuilder address = new StringBuilder();

        if( !string.IsNullOrEmpty( _listing.Address.Street ) )
        {
          address.Append( _listing.Address.Street );
          address.Append( ", " );
        }

        if( !string.IsNullOrEmpty( _listing.Address.City ) )
        {
          address.Append( _listing.Address.City );
          address.Append( ", " );
        }

        if( !string.IsNullOrEmpty( _listing.Address.Province ) )
        {
          address.Append( _listing.Address.Province );
          address.Append( ", " );
        }

        if( !string.IsNullOrEmpty( _listing.Address.PostalCode ) )
        {
          address.Append( _listing.Address.PostalCode );
          address.Append( ", " );
        }

        return address.ToString().TrimEnd( ' ', ',' );
      }
    }

    public IEnumerable<string> Ads
    {
      get
      {
        if( _details != null )
        {
          foreach( DisplayAd ad in _details.Products.OfType<DisplayAd>() )
          {
            yield return ad.Url;
          }
        }
      }
    }

    private DetailsResults _details;

    public DetailsResults Details
    {
      get { return _details; }
      set
      {
        if( value != _details )
        {
          _details = value;
          this.NotifyPropertyChanged( "Details", "Ads", "FirstAd", "HasPhone", "Phone", "Video", "WebUrl" );
        }
      }
    }

    public string FirstAd
    {
      get
      {
        if( _details != null )
        {
          foreach( DisplayAd ad in _details.Products.OfType<DisplayAd>() )
          {
            return ad.Url;
          }
        }

        return "";
      }
    }

    public bool HasAd
    {
      get { return ( _listing != null ) && ( _listing.Content.DisplayAd.Available ); }
    }

    public bool HasLogo
    {
      get { return ( _listing != null ) && ( _listing.Content.Logo.Available ); }
    }

    public bool HasPhone
    {
      get { return ( _details != null ) && ( _details.Phones.Length > 0 ); }
    }

    public bool HasPhoto
    {
      get { return ( _listing != null ) && ( _listing.Content.Photo.Available ); }
    }

    public bool HasProfile
    {
      get { return ( _listing != null ) && ( _listing.Content.Profile.Available ); }
    }

    public bool HasWebUrl
    {
      get { return ( _listing != null ) && ( _listing.Content.Url.Available ); }
    }

    public bool HasVideo
    {
      get { return ( _listing != null ) && ( _listing.Content.Video.Available ); }
    }

    public string Id
    {
      get { return ( _listing != null ) ? _listing.Id : ""; }
    }

    private Listing _listing;

    public Listing Listing
    {
      get { return _listing; }
      set
      {
        if( _listing != value )
        {
          _listing = value;
          this.NotifyPropertyChanged( "Listing", "Address", "HasAd", "HasLogo", "HasPhoto", "HasProfile", "HasProfile", "HasWebUrl", "HasVideo", "Name" );
        }
      }
    }

    public string Name
    {
      get { return ( _listing == null ) ? "" : _listing.Name; }
    }

    public string Phone
    {
      get 
      {
        if( _details != null )
        {
          Phone phone = _details.Phones.FirstOrDefault();

          if( phone != null )
          {
            return phone.DisplayNumber;
          }
        }

        return ""; 
      }
    }

    private bool _updating;

    public bool Updating
    {
      get { return _updating; }
      set
      {
        if( value != _updating )
        {
          _updating = value;
          this.NotifyPropertyChanged( "Updating" );
        }
      }
    }

    public string Video
    {
      get
      {
        if( _details != null )
        {
          foreach( Video video in _details.Products.OfType<Video>() )
          {
            string url = video.Url;

            // The YellowAPI also supports MP4 video. Simply change the .flv extension to .mp4.
            if( url.EndsWith( ".flv", StringComparison.InvariantCultureIgnoreCase ) )
            {
              url = url.Substring( 0, url.Length - 4 ) + ".mp4";
            }

            return url;
          }
        }

        return "";
      }
    }

    public string WebUrl
    {
      get
      {
        if( _details != null )
        {
          foreach( WebAddress address in _details.Products.OfType<WebAddress>() )
          {
            return address.Url;
          }
        }

        return "";
      }
    }

    public override void LoadState()
    {
      try
      {
        object value = null;

        if( PhoneApplicationService.Current.State.TryGetValue( "SelectedListingDetails", out value ) )
        {
          using( StringReader reader = new StringReader( ( string )value ) )
          {
            XmlSerializer serializer = new XmlSerializer( typeof( DetailsResults ) );
            _details = ( DetailsResults )serializer.Deserialize( reader );
          }
        }
      }
      catch { }
    }

    public override void SaveState()
    {
      // We only save our details, if any. Because this is a relatively small object, 
      // we save the XML string directly.
      if( _details != null )
      {
        try
        {
          using( StringWriter writer = new StringWriter() )
          {
            XmlSerializer serializer = new XmlSerializer( typeof( DetailsResults ) );
            serializer.Serialize( writer, _details );

            writer.Flush();

            // We assume we're only called for the selected listing. We always use the same key.
            PhoneApplicationService.Current.State[ "SelectedListingDetails" ] = writer.ToString();
          }
        }
        catch { }
      }
    }

    public void UpdateDetails()
    {
      if( _details == null )
      {
        this.Updating = true;

        try
        {
          YellowApiHelper.GetBusinessDetailsAsync(
            _listing,
            MainViewModel.Current.Language,
            new YellowApiCallback<DetailsResults>( UpdateDetailsCompleted ),
            null );
        }
        catch
        {
          this.Updating = false;
          throw;
        }
      }
    }

    private void UpdateDetailsCompleted( DetailsResults results, Exception error )
    {
      this.Dispatcher.BeginInvoke( () => 
        {
          // We ignore errors.
          this.Details = results; 
          this.Updating = false;
        } );
    }
  }
}
