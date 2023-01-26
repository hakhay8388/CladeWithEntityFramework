
export default function Actions()
{
}

Actions.EnableLoading = function()
{
  Actions.IsLoadingEnabled = true;
}

Actions.DisableLoading = function ()
{
  Actions.IsLoadingEnabled = false;
}

//Actions.EnableLoading();
Actions.DisableLoading();
