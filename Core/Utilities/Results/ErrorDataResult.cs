namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
            //geriye data döndürmek istemiyoum dersem, data yerine default vermeliyim. default demek , T türünde data demektir
        }
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
