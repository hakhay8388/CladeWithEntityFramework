
const GlobalStyles = function(_Theme) {
  return {
    ThemeProps : {
      Theme : _Theme
    },
      Grow: {
          flexGrow: 1,
      }
      ,

      BoxCenterStyle:
      {
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
      },
  };
};


export default GlobalStyles;
