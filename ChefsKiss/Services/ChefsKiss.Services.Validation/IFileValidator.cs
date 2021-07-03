namespace ChefsKiss.Services.Validation
{
    public interface IFileValidator
    {
        void ThrowIfExtensionIsInvalid(string extension);
    }
}
