namespace Wallet.Domain.Utilities
{
    public class ResultBase
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Success => !Errors.Any();

        protected ResultBase(params string[] errors) 
        { 
            Errors = errors.ToList();
        }

        protected ResultBase(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ResultBase() 
        {
            Errors = new List<string>();
        }

        public void AddError(params string[] errors)
        {
            Errors = Errors.Concat(errors);
        }
        
    }
}
