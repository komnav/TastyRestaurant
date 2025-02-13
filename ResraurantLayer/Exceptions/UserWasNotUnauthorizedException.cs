namespace RestaurantLayer.Exceptions
{
    internal class UserWasNotUnauthorizedException : Exception
    {
        public UserWasNotUnauthorizedException(
            string resourceName,
            params object[] resourceParams)
            : base($"User {resourceName} was not Unauthorized. Params: {string.Join(',', resourceParams)}")
        {

        }
    }
}
