/*whoWin = "@Model.WhoWin";*/
function checkWin(whoWin, lastBoxChildren) {
    whoWin = whoWin.substr(whoWin.length - 1);
    /*countBox = $('.boxHistoryGame').length;
    lastBox = $('.boxHistoryGame')[countBox - 1];
    lastBoxChildren = lastBox.children;*/
    i = 0;
    XO = [];

    for (var i = 0; i < lastBoxChildren.length; i++) {
        XO[i] = lastBoxChildren[i].value;
    }

    if (whoWin == "X") {
        if (XO[0] == "X" & XO[1] == "X" & XO[2] == "X") {
            $($(lastBoxChildren[0])).addClass("greenField");
            $(lastBoxChildren[1]).addClass("greenField");
            $(lastBoxChildren[2]).addClass("greenField");
        }
        if (XO[3] == "X" & XO[4] == "X" & XO[5] == "X") {
            $(lastBoxChildren[3]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[5]).addClass("greenField");
        }
        if (XO[6] == "X" & XO[7] == "X" & XO[8] == "X") {
            $(lastBoxChildren[6]).addClass("greenField");
            $(lastBoxChildren[7]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[0] == "X" & XO[3] == "X" & XO[6] == "X") {
            $(lastBoxChildren[0]).addClass("greenField");
            $(lastBoxChildren[3]).addClass("greenField");
            $(lastBoxChildren[6]).addClass("greenField");
        }
        if (XO[1] == "X" & XO[4] == "X" & XO[7] == "X") {
            $(lastBoxChildren[1]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[7]).addClass("greenField");
        }
        if (XO[2] == "X" & XO[5] == "X" & XO[8] == "X") {
            $(lastBoxChildren[2]).addClass("greenField");
            $(lastBoxChildren[5]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[0] == "X" & XO[4] == "X" & XO[8] == "X") {
            $(lastBoxChildren[0]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[2] == "X" & XO[4] == "X" & XO[6] == "X") {
            $(lastBoxChildren[2]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[6]).addClass("greenField");
        }
    }
    else {
        if (XO[0] == "O" & XO[1] == "O" & XO[2] == "O") {
            $($(lastBoxChildren[0])).addClass("greenField");
            $(lastBoxChildren[1]).addClass("greenField");
            $(lastBoxChildren[2]).addClass("greenField");
        }
        if (XO[3] == "O" & XO[4] == "O" & XO[5] == "O") {
            $(lastBoxChildren[3]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[5]).addClass("greenField");
        }
        if (XO[6] == "O" & XO[7] == "O" & XO[8] == "O") {
            $(lastBoxChildren[6]).addClass("greenField");
            $(lastBoxChildren[7]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[0] == "O" & XO[3] == "O" & XO[6] == "O") {
            $(lastBoxChildren[0]).addClass("greenField");
            $(lastBoxChildren[3]).addClass("greenField");
            $(lastBoxChildren[6]).addClass("greenField");
        }
        if (XO[1] == "O" & XO[4] == "O" & XO[7] == "O") {
            $(lastBoxChildren[1]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[7]).addClass("greenField");
        }
        if (XO[2] == "O" & XO[5] == "O" & XO[8] == "O") {
            $(lastBoxChildren[2]).addClass("greenField");
            $(lastBoxChildren[5]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[0] == "O" & XO[4] == "O" & XO[8] == "O") {
            $(lastBoxChildren[0]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[8]).addClass("greenField");
        }
        if (XO[2] == "O" & XO[4] == "O" & XO[6] == "O") {
            $(lastBoxChildren[2]).addClass("greenField");
            $(lastBoxChildren[4]).addClass("greenField");
            $(lastBoxChildren[6]).addClass("greenField");
        }
    }
}