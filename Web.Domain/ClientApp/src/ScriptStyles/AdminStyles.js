import GlobalStyles from "./GlobalStyles"
import DefaultTheme from "../Themes/DefaultTheme"


const UnLoginStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  return {
      ...__GlobalStyle,
      container: {
          maxWidth: "5000px",
          padding: "15px",
      },
      paper: {
          backgroundColor: DefaultTheme.palette.dark.darkAlternative,
          top: "80px",
          paddingBlockEnd: "80px",
      },
      paper2: {
          top: "80px"
      },
      expandedMargin: {
          margin: "0px !important",
          backgroundColor: DefaultTheme.palette.dark.darkAlternative
      }
  };
};


export default UnLoginStyles
