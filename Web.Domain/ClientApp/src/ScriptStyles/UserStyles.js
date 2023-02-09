import GlobalStyles from "./GlobalStyles"



const UserStyles = function (_Theme) {
    var __GlobalStyle = GlobalStyles(_Theme);
    return {
        ...__GlobalStyle
    };
};

export default UserStyles
