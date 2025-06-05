namespace RestaurantLayer.Exceptions;

public class ResourceAlreadyExist(
    string resourceName,
    params object[] resourceParams)
    : Exception($"Resource {resourceName} was not created(Duplicate). Params: {string.Join(',', resourceParams)}");