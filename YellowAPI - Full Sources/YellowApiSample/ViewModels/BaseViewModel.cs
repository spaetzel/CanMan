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
using System.ComponentModel;
using System.Windows.Threading;

namespace YellowApiSample.ViewModels
{
  public abstract class BaseViewModel : INotifyPropertyChanged
  {
    public BaseViewModel()
    {
    }

    private static Dispatcher __dispatcher;

    protected Dispatcher Dispatcher
    {
      get
      {
        if( __dispatcher == null )
        {
          __dispatcher = Deployment.Current.Dispatcher;
        }

        return __dispatcher;
      }
    }

    public bool IsInDesignTime
    {
      get { return System.ComponentModel.DesignerProperties.IsInDesignTool; }
    }

    public abstract void LoadState();

    public abstract void SaveState();

    protected void SafeBeginInvoke( Action action )
    {
      if( this.Dispatcher.CheckAccess() )
      {
        action();
      }
      else
      {
        this.Dispatcher.BeginInvoke( action );
      }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged( params string[] properties )
    {
      PropertyChangedEventHandler handlers = this.PropertyChanged;

      if( ( properties != null ) && ( handlers != null ) )
      {
        foreach( string property in properties )
        {
          this.SafeBeginInvoke( () =>
            {
              handlers( this, new PropertyChangedEventArgs( property ) );
            } );
        }
      }
    }

    #endregion
  }
}
