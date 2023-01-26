
import GlobalStyles from "./GlobalStyles"

const MaterialTableStyles = function(_Theme) {
  var __GlobalStyle = GlobalStyles(_Theme);
  return {
    ...__GlobalStyle,
    title: {
      fontSize: "20px",
      fontWeight: 500,
      [_Theme.breakpoints.only('xs')]: {
        fontSize: "10px",
        marginRight: "25px"
      },
    },
    searchAreaStyle :{
      [_Theme.breakpoints.only('xs')]: {
        display: "none"
      },
    }
  };
};




export default MaterialTableStyles
