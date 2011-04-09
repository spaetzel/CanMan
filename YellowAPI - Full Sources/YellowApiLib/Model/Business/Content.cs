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
  /// A class that indicates what information could be found when querying for a business' details.
  /// </summary>
  public class Content
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Content()
    {
    }

    #region INNER TYPES

    /// <summary>
    /// A class that indicates if specific information is available or not.
    /// </summary>
    public class ContentInfo
    {
      /// <summary>
      /// Is the given information available?
      /// </summary>
      [XmlAttribute( "avail" )]
      public bool Available { get; set; }

      /// <summary>
      /// Indicates that the merchand paid to be part of this result.
      /// </summary>
      [XmlAttribute( "inMkt" )]
      public bool InMarket { get; set; }
    }

    #endregion

    #region Video PROPERTY

    private ContentInfo _video = new ContentInfo();

    /// <summary>
    /// Video availability information.
    /// </summary>
    [XmlElement( "Video" )]
    public ContentInfo Video 
    {
      get { return _video; }
      set { _video = value ?? new ContentInfo(); }
    }

    #endregion

    #region Photo PROPERTY

    private ContentInfo _photo = new ContentInfo();

    /// <summary>
    /// Pictures availability information.
    /// </summary>
    [XmlElement( "Photo" )]
    public ContentInfo Photo 
    { 
      get { return _photo; }
      set { _photo = value ?? new ContentInfo(); }
    }

    #endregion

    #region Profile PROPERTY

    private ContentInfo _profile = new ContentInfo();

    /// <summary>
    /// Keywords availability information.
    /// </summary>
    [XmlElement( "Profile" )]
    public ContentInfo Profile 
    {
      get { return _profile; }
      set { _profile = value ?? new ContentInfo(); }
    }

    #endregion

    #region DisplayAd PROPERTY

    private ContentInfo _displayAd = new ContentInfo();

    /// <summary>
    /// Advertising image availability information.
    /// </summary>
    [XmlElement( "DspAd" )]
    public ContentInfo DisplayAd 
    {
      get { return _displayAd; }
      set { _displayAd = value ?? new ContentInfo(); }
    }

    #endregion

    #region Url PROPERTY

    private ContentInfo _url = new ContentInfo();

    /// <summary>
    /// Web site address availability information.
    /// </summary>
    [XmlElement( "Url" )]
    public ContentInfo Url
    {
      get { return _url; }
      set { _url = value ?? new ContentInfo(); }
    }

    #endregion

    #region Logo PROPERTY

    private ContentInfo _logo = new ContentInfo();

    /// <summary>
    /// Logo image availability information.
    /// </summary>
    [XmlElement( "Logo" )]
    public ContentInfo Logo 
    {
      get { return _logo; }
      set { _logo = value ?? new ContentInfo(); }
    }

    #endregion
  }
}
