import { createTheme  } from "@mui/material/styles";

const TestTheme = createTheme ({
  palette: {
    primary: {
      main: "#ff5757",
      dark: "#B23C3C"
    },
    danger: {
      main: "#ff2020",
    }
    ,
    light: {
      main: "#00BCD4",
    }
    ,
    dark: {
      main: "#212121",
      light: "#4d4d4d",
      contrastText: "#303030",
      alternative: '#000000'
    }
    ,
    secondary: {
      main: '#a2cf6e',
      dark: '#71904d',
      contrastText: '#FFFFFF',
    },
    success :{
      /*
            main: '#618833'
      */
      main: '#a2cf6e',
      contrastText: '#FFFFFF',
    }
    ,
    link: {
      main: '#9C27B0'
    }
    ,
    default: {
      main: '#F0F0F0',
      light: "#FEFEFE",
      contrastText: "#fdfdfd",
      alternative: '#ffeeee'
    },
    info : {
      dark: '#2582ac',
      main: '#35baf6',
      light: '#5dc7f7',
      contrastText: '#2548A5',
      alternative: '#6490b1'
    },
    error: {
      main: "#ff5757",
    },
    warning : {
      main: "#ff9100"
    },
    none : {
      main: "#ffffff"
    },
    action : {
      main: '#35baf6',
    }
  },
  typography : {
    fontFamily: "Montserrat",
    button: {
      textTransform: "none",
      textDecoration: "none",
    }
  }
});

export default TestTheme;
