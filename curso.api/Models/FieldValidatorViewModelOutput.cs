namespace curso.api.Models
{
    public class FieldValidatorViewModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public FieldValidatorViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
