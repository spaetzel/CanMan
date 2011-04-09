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
  /// A class that represents summary information about a business search.
  /// </summary>
  public class Summary
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Summary()
    {
    }
    
    #region Search PROPERTY

    private Search _search = new Search();

    /// <summary>
    /// The criteria used for a search.
    /// </summary>
    [XmlElement( "Search" )]
    public Search Search 
    {
      get { return _search; }
      set { _search = value ?? new Search(); }
    }

    #endregion

    #region ListingEntries PROPERTY

    private ListingEntries _listingEntries = new ListingEntries();

    /// <summary>
    /// Listing entries rank for a given search.
    /// </summary>
    [XmlElement( "ListingEntries" )]
    public ListingEntries ListingEntries 
    {
      get { return _listingEntries; }
      set { _listingEntries = value ?? new ListingEntries(); }
    }

    #endregion

    #region Paging PROPERTY

    private Paging _paging = new Paging();

    /// <summary>
    /// Paging information for a given search.
    /// </summary>
    [XmlElement( "Paging" )]
    public Paging Paging 
    {
      get { return _paging; }
      set { _paging = value ?? new Paging(); }
    }

    #endregion
  }
}
