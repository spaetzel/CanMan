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
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace YellowPages.YellowApi.Business
{
  /// <summary>
  /// A class that represents the results of a YellowAPI search query.
  /// </summary>
  /// <remarks>Calls to FindBusinessAsync and GetDealersAsync return their results in this type of object via their callback.</remarks>
  [XmlRoot( Namespace = "", IsNullable = false )]
  public class SearchResults
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public SearchResults()
    {
    }

    #region Summary PROPERTY

    private Summary _summary = new Summary();

    /// <summary>
    /// Summary information about the search query.
    /// </summary>
    [XmlElement( "Summary" )]
    public Summary Summary 
    {
      get { return _summary; }
      set { _summary = value ?? new Summary(); }
    }

    #endregion

    #region Listings PROPERTY

    private Listing[] _listings = new Listing[ 0 ];

    /// <summary>
    /// List of businesses matching the search criteria.
    /// </summary>
    [XmlArray( "Listings" )]
    [XmlArrayItem( "Listing" )]
    public Listing[] Listings 
    {
      get { return _listings; }
      set { _listings = value ?? new Listing[ 0 ]; }
    }

    #endregion
  }
}
