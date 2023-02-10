import { createTheme } from "@mui/material/styles";

const DefaultTheme = createTheme({

    transitions: {
        duration: {
            enteringScreen: 300,
            leavingScreen: 300,
        }
        ,
        easing: {
            // This is the most common easing curve.
            easeInOut: 'cubic-bezier(0.4, 0, 0.6, 1)', //'cubic-bezier(0.4, 0, 0.2, 1)',
            // Objects enter the screen at full velocity from off-screen and
            // slowly decelerate to a resting point.
            easeOut: 'cubic-bezier(0.4, 0, 0.6, 1)', // 'cubic-bezier(0.0, 0, 0.2, 1)',
            // Objects leave the screen at full velocity. They do not decelerate when off-screen.
            easeIn: 'cubic-bezier(0.4, 0, 0.6, 1)',  //'cubic-bezier(0.4, 0, 1, 1)',
            // The sharp curve is used by objects that may return to the screen at any time.
            sharp: 'cubic-bezier(0.4, 0, 0.6, 1)',
        },
    },
    components: {
        MuiDrawer: {
            styleOverrides: {
                paper: {
                    background: '#1E1F21'
                }
            }
        }
    },
    /*,
palette: {
primary: {
  main: "#ff5757",
  dark: "#B23C3C",
  light: "#ff6a6a"
},
danger: {
  main: "#ff5757",
}
,
light: {
  main: "#00BCD4",
  contrastText: "#DADCE0"
}
,
dark: {
  main: "#212121",
  light: '#495057',
  contrastText: "#303030",
  alternative: '#000000',
  lightAlternative: '#3C4858',
  darkAlternative: '#1E1F21',
  contrastAlternative: "#333333",
  contrastLight: "#4D4D4D",
  contrastDark: "#7A7A7A",
  secondAlternative : "#29303B",
  thirdAlternative: "#464646",
  fourthAlternative: "#3a4248"
}
,
secondary: {
  main: '#a2cf6e',
  dark: '#71904d',
  contrastText: '#FFFFFF',
  light: "#2EFF22",
  alternativeDark: "#117520",
  alternativeLight: "#64c270",
  secondAlternative: "#4dbd74",
},
success :{
  main: '#a2cf6e',
  contrastText: '#FFFFFF',
  light: "#E0E0E0"
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
  alternative: '#ffeeee',
  darkAlternative: '#b1b1b1',
  lightAlternative: '#f1f1f1',
  contrastAlternative: "#cccccc",
  contrastDark: "#9C9C9C",
  contrastLight: "#999999",
  mainAlternative: "#f2f2f6",
  mainLight: "#FAFAFC",
  mainDark: "#707070",
  secondAlternative: "#505050",
  thirdAlternative: "#A0A0A0",
  fourthAlternative: "#d0d0d9",
  fifthAlternative: "#c5c5c5",
  sixthAlternative: "#f7f7f7"
},
info : {
  dark: '#2582ac',
  main: '#35baf6',
  light: '#5dc7f7',
  contrastText: '#2548A5',
  alternative: '#6490b1',
  lightAlternative: "#1B9CE2",
  darkAlternative: "#4C5776",
  contrastAlternative: "#238AFF",
  secondAlternative: "#348dec"
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
},*/
    palette: {
        primary: {
            main: "#ff5757",
            dark: "#B23C3C",
            light: "#ff6a6a"
        },
        secondary: {
            main: '#a2cf6e',
            dark: '#71904d',
            contrastText: '#FFFFFF',
            light: "#2EFF22",
            alternativeDark: "#117520",
            alternativeLight: "#64c270",
            secondAlternative: "#4dbd74",
        },
        success: {
            main: '#a2cf6e',
            contrastText: '#FFFFFF',
            light: "#E0E0E0"
        }
        ,
        info: {
            dark: '#2582ac',
            main: '#35baf6',
            light: '#5dc7f7',
            contrastText: '#2548A5',
            alternative: '#6490b1',
            lightAlternative: "#1B9CE2",
            darkAlternative: "#4C5776",
            contrastAlternative: "#238AFF",
            secondAlternative: "#348dec"
        },
        error: {
            main: "#ff5757",
        },
        warning: {
            main: "#ff9100"
        },
        danger: {
            main: "#ff5757",
        }
        ,
        light: {
            main: "#00BCD4",
            contrastText: "#DADCE0"
        }
        ,
        dark: {
            main: "#212121",
            light: '#495057',
            contrastText: "#303030",
            alternative: '#000000',
            lightAlternative: '#3C4858',
            darkAlternative: '#1E1F21',
            contrastAlternative: "#333333",
            contrastLight: "#4D4D4D",
            contrastDark: "#7A7A7A",
            secondAlternative: "#29303B",
            thirdAlternative: "#464646",
            fourthAlternative: "#3a4248"
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
            alternative: '#ffeeee',
            darkAlternative: '#b1b1b1',
            lightAlternative: '#f1f1f1',
            contrastAlternative: "#cccccc",
            contrastDark: "#9C9C9C",
            contrastLight: "#999999",
            mainAlternative: "#f2f2f6",
            mainLight: "#FAFAFC",
            mainDark: "#707070",
            secondAlternative: "#505050",
            thirdAlternative: "#A0A0A0",
            fourthAlternative: "#d0d0d9",
            fifthAlternative: "#c5c5c5",
            sixthAlternative: "#f7f7f7"
        },
        none: {
            main: "#ffffff"
        },
        action: {
            main: '#35baf6',
        }
    }
    ,
    typography: {
        fontFamily: "Arial",
        button: {
            textTransform: "none",
            textDecoration: "none",
        }
    }
});

export default DefaultTheme;
