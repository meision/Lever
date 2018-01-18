using System;
using System.Collections.Generic;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;

namespace $rootnamespace$
{
    public sealed class $safeitemrootname$ : IDisposable
    {
        #region Static
        #endregion Static

        #region Fields & Properites
        #endregion Fields & Properites

        #region Constructors & Destructors
        private static readonly $safeitemrootname$ __instance = new $safeitemrootname$();
        public static $safeitemrootname$ Instance
        {
            get
            {
                return $safeitemrootname$.__instance;
            }
        }

        private $safeitemrootname$()
        {
        }
        
        public void Initialize()
        {
            // TODO: Add extra Initialize logic here.
        }

        ~$safeitemrootname$()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: Release managed resources
            }

            // TODO: Release unmanaged resources
        }
        #endregion Constructors & Destructors

        #region Events
        #endregion Events

        #region Methods
        #endregion Methods
    }
}
