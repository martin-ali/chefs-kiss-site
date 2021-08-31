namespace ChefsKiss.Common
{
    public class Helpers
    {
        // TODO: This is ugly code. Make it better
        public static string ControllerName<ControllerType>()
        {
            var type = typeof(ControllerType).Name;
            var controllerName = type.Replace("Controller", string.Empty);

            return controllerName;
        }

        public static string ReadableFileSize(int bytes)
        {
            var data = bytes;
            var units = new string[] { "B", "KB", "MB", "GB", "TB" };
            var unitIndex = 0;

            while (data > 1024)
            {
                data /= 1024;
                unitIndex++;
            }

            var unit = units[unitIndex];
            var size = $"{data}{unit}";

            return size;
        }
    }
}
