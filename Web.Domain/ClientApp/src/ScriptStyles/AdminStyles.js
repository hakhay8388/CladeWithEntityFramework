import GlobalStyles from "./GlobalStyles"
import DefaultTheme from "../Themes/DefaultTheme"


const UnLoginStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  return {
      ...__GlobalStyle,
  };
};


export default UnLoginStyles
