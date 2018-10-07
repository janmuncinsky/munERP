namespace MunCode.Core.Refection.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Reflection;
    using MunCode.Core.Reflection.Scanning;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel;
    using MunCode.Core.Wpf.DocumentModel.Menu;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
    public static class TypeScanResultEnumerableExtensions
    {
        private const string ViewModelConvention = "ViewModel";

        public static void RegisterViewModels(
            this IEnumerable<TypeScanResult> enumerable,
            IRegisterCallbacks callbacks)
        {
            Guard.NotNull(callbacks, nameof(callbacks));
            Guard.NotNull(enumerable, nameof(enumerable));

            foreach (var typeScanResult in enumerable)
            {
                if (typeScanResult.Type.Name.Contains(ViewModelConvention))
                {
                    callbacks.SingletonRegisterCallback(typeScanResult.Type, typeScanResult.Type);
                }
            }
        }

        public static IEnumerable<TypeScanResult> RegisterDocuments(
            this IEnumerable<TypeScanResult> enumerable,
            IRegisterCallbacks callbacks)
        {
            foreach (var typeScanResult in enumerable)
            {
                if (typeof(IDocument).IsAssignableFrom(typeScanResult.Type))
                {
                    callbacks.CallScopeRegisterCallback(typeScanResult.Type, typeScanResult.Type);
                }
                else
                {
                    yield return typeScanResult;
                }
            }
        }

        public static IEnumerable<TypeScanResult> RegisterMenu(
            this IEnumerable<TypeScanResult> enumerable,
            IRegisterCallbacks callbacks)
        {
            return enumerable.RegisterComponents<IMenuComponent>(callbacks.SingletonRegisterCallback);
        }

        public static IEnumerable<TypeScanResult> RegisterStatusBar(
            this IEnumerable<TypeScanResult> enumerable,
            IRegisterCallbacks callbacks)
        {
            return enumerable.RegisterComponents<IStatusBarViewModel>(callbacks.SingletonRegisterCallback);
        }

        public static IEnumerable<TypeScanResult> RegisterDialogs(
            this IEnumerable<TypeScanResult> enumerable,
            IRegisterCallbacks callbacks)
        {
            return enumerable.RegisterComponents(typeof(IHaveDialogResult<>), callbacks.CallScopeRegisterCallback);
        }

        private static IEnumerable<TypeScanResult> RegisterComponents<TComponent>(
            this IEnumerable<TypeScanResult> enumerable,
            ContainerRegisterCallback callback)
        {
            return enumerable.RegisterComponents(typeof(TComponent), callback);
        }

        private static IEnumerable<TypeScanResult> RegisterComponents(
            this IEnumerable<TypeScanResult> enumerable,
            Type component,
            ContainerRegisterCallback callback)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            Guard.NotNull(component, nameof(component));
            Guard.NotNull(callback, nameof(callback));

            foreach (var typeScanResult in enumerable)
            {
                var type = typeScanResult.Type;
                var service = type.GetInterfaces().FirstOrDefault(t => t.GetTypeDefinition() == component);
                if (service != null)
                {
                    callback(service, type);
                }
                else
                {
                    yield return typeScanResult;
                }
            }
        }
    }
}