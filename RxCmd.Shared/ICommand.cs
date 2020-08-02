// -----------------------------------------------------------------------------
//  <copyright file="ICommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------


using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace RxCmd.Shared
{
    [InheritedExport(typeof(ICommand))]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a shared class library.")]
    public interface ICommand
    {
        string Name { get; }

        string[] Aliases { get; }

        string Description { get; }

        void Execute(params object[] args);
    }
}