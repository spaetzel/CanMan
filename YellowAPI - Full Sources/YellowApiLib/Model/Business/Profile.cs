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
  /// A class that represents extra textual information about a business.
  /// </summary>
  public class Profile : LocalizedProduct
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Profile()
    {
    }

    #region Keywords PROPERTY

    private Keyword[] _keywords = new Keyword[ 0 ];

    /// <summary>
    /// Keywords providing extra information about a business:
    /// <list type="">
    /// <item>Opening hours</item>
    /// <item>Payment methods</item>
    /// <item>Languages spoken</item>
    /// <item>Instructions about getting there</item>
    /// <item>Products and services</item>
    /// <item>Specialties</item>
    /// <item>Brands carried</item>
    /// <item>Types of cuisine</item>
    /// </list>
    /// </summary>
    /// <remarks>Using System.Linq, you can easily locate the information you need.
    /// 
    /// <code>
    /// Profile profile = details.Products.FirstOrDefault&lt;Profile&gt;();
    /// 
    /// if( profile != null )
    /// {
    ///   foreach( OpenHoursKeyword hours in profile.Keywords.OfType&lt;OpenHoursKeyword&gt;() )
    ///   {
    ///     // Your code
    ///   }
    /// }
    /// </code>
    /// 
    /// Possible keyword types are: <seealso cref="OpenHoursKeyword"/>, <seealso cref="PaymentMethodKeyword"/>, 
    /// <seealso cref="SpokenLanguageKeyword"/>, <seealso cref="GettingThereKeyword"/>, <seealso cref="ProductsAndServicesKeyword"/>,
    /// <seealso cref="SpecialtiesKeyword"/>, <seealso cref="BrandCarriedKeyword"/> and <seealso cref="CuisineTypeKeyword"/>.</remarks>
    [XmlArrayItem( typeof( OpenHoursKeyword ), ElementName = "OpenHrs" )]
    [XmlArrayItem( typeof( PaymentMethodKeyword ), ElementName = "MthdPmt" )]
    [XmlArrayItem( typeof( SpokenLanguageKeyword ), ElementName = "LangSpk" )]
    [XmlArrayItem( typeof( GettingThereKeyword ), ElementName = "GetThr" )]
    [XmlArrayItem( typeof( ProductsAndServicesKeyword ), ElementName = "ProdServ" )]
    [XmlArrayItem( typeof( SpecialtiesKeyword), ElementName = "Special" )]
    [XmlArrayItem( typeof( BrandCarriedKeyword ), ElementName = "BrndCrrd" )]
    [XmlArrayItem( typeof( CuisineTypeKeyword ), ElementName = "CuisineTp" )]
    public Keyword[] Keywords
    {
      get { return _keywords; }
      set { _keywords = value ?? new Keyword[ 0 ]; }
    }

    #endregion
  }
}
