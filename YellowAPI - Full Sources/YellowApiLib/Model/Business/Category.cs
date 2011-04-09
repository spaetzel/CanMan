﻿/*
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
  /// This class represents a category a business is part of.
  /// </summary>
  public class Category
  {
    /// <summary>
    /// Default constructor.
    /// </summary>
    public Category()
    {
    }

    #region Id PROPERTY

    private string _id = "";

    /// <summary>
    /// Unique category ID.
    /// </summary>
    [XmlAttribute( "id" )]
    public string Id
    {
      get { return _id; }
      set { _id = value ?? ""; }
    }

    #endregion

    #region IsSensitive PROPERTY

    /// <summary>
    /// Indicates if the content is for adults only.
    /// </summary>
    [XmlAttribute( "isSensitive" )]
    public bool IsSensitive { get; set; }

    #endregion

    #region Name PROPERTY

    private string _name = "";

    /// <summary>
    /// Category name.
    /// </summary>
    [XmlText]
    public string Name
    {
      get { return _name; }
      set { _name = value ?? ""; }
    }

    #endregion
  }
}