namespace ChefsKiss.Common
{
    using System;

    public class Helpers
    {
        // TODO: This is ugly code, make it better
        public static string GetControllerName<ControllerType>()
        {
            var type = typeof(ControllerType).Name;
            var controllerName = type.Replace("Controller", string.Empty);

            return controllerName;
        }
    }
}
