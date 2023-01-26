
import GlobalStyles from "./GlobalStyles"

const InfoModalStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  return {
    ...__GlobalStyle,
    dialog: {
      position: 'absolute',
      top: 50,
      margin : '0px !important'
    }
  };
};




export default InfoModalStyles
