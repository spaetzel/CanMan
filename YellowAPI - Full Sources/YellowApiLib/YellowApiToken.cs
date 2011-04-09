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

namespace YellowPages.YellowApi
{
  /// <summary>
  /// A class that represents a token returned after a call to the YellowAPI via <seealso cref="YellowApiHelper"/>. 
  /// Can be used to abort an operation that is taking too long.
  /// </summary>
  /// <typeparam name="T">The search results type, either <seealso cref="Business.SearchResults"/> or <seealso cref="Business.DetailsResults"/>.</typeparam>
  public class YellowApiToken<T>
    where T : class
  {
    internal YellowApiToken( HttpWebRequest request, YellowApiCallback<T> userCallback, object userState )
    {
      if( request == null )
        throw new ArgumentNullException( "request" );

      this.Request = request;
      this.UserCallback = userCallback;
      this.UserState = userState;
    }

    #region Request PROPERTY

    internal HttpWebRequest Request { get; private set; }

    #endregion

    #region UserCallback PROPERTY

    internal YellowApiCallback<T> UserCallback { get; private set; }

    #endregion

    #region UserState PROPERTY

    internal object UserState { get; private set; }

    #endregion

    /// <summary>
    /// Abort the current search operation.
    /// </summary>
    public void Abort()
    {
      this.Request.Abort();
    }
  }
}
