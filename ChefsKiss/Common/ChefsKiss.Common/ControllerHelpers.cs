namespace ChefsKiss.Common
{
    public class ControllerHelpers
    {
        public static string GetControllerName(string controller)
        {
            return controller.Replace("Controller", string.Empty);
        }
    }
}
