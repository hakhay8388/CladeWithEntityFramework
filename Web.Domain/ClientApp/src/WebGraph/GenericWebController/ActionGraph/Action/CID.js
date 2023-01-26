function CID(_ActionID, _Data, _SessionID)
{
  this.CID = _ActionID;
  this.Data = _Data;
  this.SessionID = _SessionID;
}

CID.prototype.toJSONString = function (filter) 
{
  return JSON.stringify(this, filter);
};

export default CID;
