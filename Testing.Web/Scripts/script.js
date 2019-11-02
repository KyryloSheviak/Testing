 function countdown() {
    seconds = document.getElementById("timerLabel").innerHTML;
    if (seconds > 0) {
        document.getElementById("timerLabel").innerHTML = seconds - 1;
        setTimeout("countdown()", 1000 * 60);
    } else {
        alert("Время вышло!\nРезультаты сохранены.\nВы будете перенаправлены на страницу с тестами.");
        window.location.replace("/User/Tests");
    }
}
setTimeout("countdown()", 1000 * 60);
/*
function initializeTimer() {
    var endDate = new Date(2018, 2, 28);

    var currentDate = new Date();
    var seconds = (endDate - currentDate) / 1000;
    if (seconds > 0) {
        var minutes = seconds / 60;
        var hours = minutes / 60;
        var days = hours / 24;
        minutes = (hours - Math.floor(hours)) * 60;
        days = Math.floor(days);
        hours = Math.floor(hours) - days * 24;

        seconds = Math.floor((minutes - Math.floor(minutes)) * 60);
        minutes = Math.floor(minutes);

        setTimePage(days, hours, minutes, seconds);

        function secOut() {
            if (seconds == 0) {
                if (minutes == 0) {
                    if (hours == 0) {
                        if (days == 0) {
                            showMessage(timerId);
                        }
                        else {
                            days--;
                            hours = 24;
                            minutes = 59;
                            seconds = 59;
                        }
                    }
                    else {
                        hours--;
                        minutes = 59;
                        seconds = 59;
                    }
                }
                else {
                    minutes--;
                    seconds = 59;
                }
            }
            else {
                seconds--;
            }
            setTimePage(days, hours, minutes, seconds);
        }
        timerId = setInterval(secOut, 1000)
    }
    else {
        alert("Установленая дата уже прошла");
    }
}

window.onload = function () {
    initializeTimer();
}

function setTimePage(d, h, m, s) {
    var days = document.getElementById("days");
    var hours = document.getElementById("hours");
    var minutes = document.getElementById("minutes");
    var seconds = document.getElementById("seconds");

    days.innerHTML = d;
    hours.innerHTML = h;
    minutes.innerHTML = m;
    seconds.innerHTML = s;

}
*/

// ====
$(function () {
    $('#addLink').on("click", function () {
        i++;
        var html2Add =
            "<div class='answerItem'>" +
            "<h5>Ответ № " + (i + 1) + "</h5>" +
            "<div>" +
            "<input type='text' name='answers' id='answers' class='form-control text-box single-line' required />" +
            "<input type='checkbox' onClick='document.getElementById('ans_" + i + "').value = this.checked ? 1 : 0;'> Правильный?" +
            "<input type='hidden' name='ans' id='ans_" + i + "' value='0'>" +
            "</div> " +
            "</div>";
        $('#answersBlock').append(html2Add);
    })
});

function OnSuccess() {
    alert("Записи в БД были добавлены!");
    $('#form0').empty();
    $("#form0").html(`
                <div class="QuestionBlock">
                    <label>Вопрос</label>
                    <input type="text" name="name" class="form-control text-box single-line" required />
                </div>
                <div id="answersBlock">
                    <div class="answerItem">
                        <h5>Ответ № 1</h5>
                        <div>
                            <input type="text" name="answers" id="answers" class="form-control text-box single-line" required />
                            <input type="checkbox" onClick="document.getElementById('ans_1').value = this.checked ? 1 : 0;"> Правильный?
                            <input type="hidden" name="ans" id="ans_1" value="0">
                        </div>
                    </div>
                </div>
                <br />
                <input id="test" type="submit" value="Добавить вопрос" class="btn btn-primary" />
                <input type="text" name="idtest" value="@Model.Id" hidden />`);
    i = 0;
}