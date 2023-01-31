import GlobalStyles from "./GlobalStyles"


const UnLoginStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  return {
    ...__GlobalStyle,
    test: {
      position: 'absolute',
      top: 50,
      margin : '0px !important'
    }
  };
};


export default UnLoginStyles
