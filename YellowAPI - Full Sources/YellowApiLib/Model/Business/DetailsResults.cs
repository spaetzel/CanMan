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
using System.Xml.Serialization;

namespace YellowPages.YellowApi.Business
{
  /// <summary>
  /// A class that exposes details about a business.
  /// </summary>
  [XmlRoot( Namespace = "", ElementName = "Listing", IsNullable = false )]
  public class DetailsResults
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public DetailsResults()
    {
    }

    #region Id PROPERTY

    private string _id = "";

    /// <summary>
    /// Unique ID identifying the business.
    /// </summary>
    [XmlAttribute( "id" )]
    public string Id
    {
      get { return _id; }
      set { _id = value ?? ""; }
    }

    #endregion

    #region Name PROPERTY

    private string _name = "";

    /// <summary>
    /// Business name.
    /// </summary>
    public string Name
    {
      get { return _name; }
      set { _name = value ?? ""; }
    }

    #endregion

    #region Address PROPERTY

    private Address _address = new Address();

    /// <summary>
    /// Business address.
    /// </summary>
    public Address Address
    {
      get { return _address; }
      set { _address = value ?? new Address(); }
    }

    #endregion

    #region Phones PROPERTY

    private Phone[] _phones = new Phone[ 0 ];

    /// <summary>
    /// Phone numbers.
    /// </summary>
    [XmlArrayItem( ElementName = "Phone" )]
    public Phone[] Phones
    {
      get { return _phones; }
      set { _phones = value ?? new Phone[ 0 ]; }
    }

    #endregion

    #region Coordinates PROPERTY

    private GeoCode _coordinates = new GeoCode();

    /// <summary>
    /// Geological position of the business.
    /// </summary>
    [XmlElement( ElementName = "GeoCode" )]
    public GeoCode Coordinates
    {
      get { return _coordinates; }
      set { _coordinates = value ?? new GeoCode(); }
    }

    #endregion

    #region Categories PROPERTY

    private Category[] _categories = new Category[ 0 ];

    /// <summary>
    /// Categories this business belongs to.
    /// </summary>
    [XmlArrayItem( ElementName = "Category" )]
    public Category[] Categories
    {
      get { return _categories; }
      set { _categories = value ?? new Category[ 0 ]; }
    }

    #endregion

    #region MerchantUrl PROPERTY

    private string _merchantUrl = "";

    /// <summary>
    /// Web address on yellowpages.ca or pagesjaunes.ca about this business.
    /// </summary>
    public string MerchantUrl
    {
      get { return _merchantUrl; }
      set { _merchantUrl = value ?? ""; }
    }

    #endregion

    #region Logos PROPERTY

    private Logo[] _logos = new Logo[ 0 ];

    /// <summary>
    /// Logos.
    /// </summary>
    [XmlArrayItem( ElementName = "Logo" )]
    public Logo[] Logos
    {
      get { return _logos; }
      set { _logos = value ?? new Logo[ 0 ]; }
    }

    #endregion

    #region Products PROPERTY

    private Product[] _products = new Product[ 0 ];

    /// <summary>
    /// Extra product information, like profile keywords, display ads, pictures, video and web site address.
    /// </summary>
    /// <remarks>Using System.Linq, it's easy to extract the product information you need.
    /// 
    /// <code>foreach( DisplayAd ad in _details.Products.OfType&lt;DisplayAd&gt;() ) { ... }
    /// 
    /// WebAddress firstUrl = _details.Products.OfType&lt;WebAddress&gt;().FirstOrDefault();
    /// if( firstUrl != null ) { ... }
    /// </code>
    /// 
    /// Possible product types are: <code>Profile</code>, <code>DisplayAd</code>, <code>Photo</code>, <code>Video</code> and <code>WebAddress</code>.
    /// </remarks>
    [XmlArrayItem( typeof( Profile ), ElementName = "Profile" )]
    [XmlArrayItem( typeof( DisplayAd ), ElementName = "DispAd" )]
    [XmlArrayItem( typeof( Photo ), ElementName = "Photo" )]
    [XmlArrayItem( typeof( Video ), ElementName = "Video" )]
    [XmlArrayItem( typeof( WebAddress ), ElementName = "WebUrl" )]
    public Product[] Products
    {
      get { return _products; }
      set { _products = value ?? new Product[ 0 ]; }
    }

    #endregion
  }
}
