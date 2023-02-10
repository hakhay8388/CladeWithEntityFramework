import GlobalStyles from "./GlobalStyles"

const MaterialTableStyles = (_Theme) => ({
  ...GlobalStyles(_Theme),
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
});


export default MaterialTableStyles
