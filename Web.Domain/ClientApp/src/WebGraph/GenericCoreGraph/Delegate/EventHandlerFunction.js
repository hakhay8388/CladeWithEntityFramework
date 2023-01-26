var EventHandlerFunction = function (_Delegate)
{
	var Delegate = _Delegate;

	return function (_Event)
	{
		Delegate.Run(_Event);
	}
}

export default EventHandlerFunction