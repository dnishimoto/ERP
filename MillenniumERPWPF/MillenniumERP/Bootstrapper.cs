//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Listensoftware">
//     Copyright (c) Listensoftware Pte. Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MillenniumERP
{
    using System.Windows;
    using Caliburn.Micro;
    using MillenniumERP.ViewModels;

    /// <summary>
    /// Interaction logic for the Bootstrapper
    /// </summary>
    /// <seealso cref="Caliburn.Micro.BootstrapperBase" />
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
