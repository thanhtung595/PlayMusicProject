$.ajax({
    url: "/Home/Index_GetMusic",
    type: "GET",
    contentType: "application/json",
    beforeSend: function () {

    },
    success: function (res) {
        if (res && res.length > 0) {
            $('.index-GetMusic_Top15').html('');
            $('.index-GetMusic_Top610').html('');
            $('.index-GetMusic_Top1115').html('');
            for (let i = 0; i < res.length; i++) {
                let music = res[i];
                let cnt = i + 1;
                let stt;
                if (cnt <= 9) {
                    stt = "0" + cnt;
                } else {
                    stt = cnt;
                }
                if (i < 5) {
                    let html = `
                        <div class="ms_weekly_box">
                            <div class="weekly_left">
                                <span class="w_top_no">
                                ${stt}
                                </span>
                                <div class="w_top_song">
                                    <div class="w_tp_song_img">

                                        <img src="/images/ImageMusic/${music.imageMusic}" alt="" class="img-fluid">
                                        <div class="ms_song_overlay">
                                        </div>
                                        <div class="ms_play_icon">
                                            <a href="/Home/Index?id=${music.idMusic}"><img src="/images/svg/play.svg" alt=""></a>
                                        </div>
                                    </div>
                                    <div class="w_tp_song_name">
                                        <h3><a href="#">${music.nameMusic}</a></h3>
                                        <p>${music.nameArtists}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ms_divider"></div> `;
                    $('.index-GetMusic_Top15').append(html);
                } else if (i >= 5 && i < 10) {
                    let html = `
                        <div class="ms_weekly_box">
                            <div class="weekly_left">
                                <span class="w_top_no">
                                    ${stt}
                                </span>
                                <div class="w_top_song">
                                    <div class="w_tp_song_img">

                                        <img src="/images/ImageMusic/${music.imageMusic}" alt="" class="img-fluid">
                                        <div class="ms_song_overlay">
                                        </div>
                                        <div class="ms_play_icon">
                                            <a href="/Home/Index?id=${music.idMusic}"><img src="/images/svg/play.svg" alt=""></a>
                                        </div>
                                    </div>
                                    <div class="w_tp_song_name">
                                        <h3><a href="#">${music.nameMusic}</a></h3>
                                        <p>${music.nameArtists}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ms_divider"></div> `;
                    $('.index-GetMusic_Top610').append(html);
                } else if (i > 9 && i < 15) {
                    let html = `
                        <div class="ms_weekly_box">
                            <div class="weekly_left">
                                <span class="w_top_no">
                                    ${stt}
                                </span>
                                <div class="w_top_song">
                                    <div class="w_tp_song_img">

                                        <img src="/images/ImageMusic/${music.imageMusic}" alt="" class="img-fluid">
                                        <div class="ms_song_overlay">
                                        </div>
                                        <div class="ms_play_icon">
                                            <a href="/Home/Index?id=${music.idMusic}"><img src="/images/svg/play.svg" alt=""></a>
                                        </div>
                                    </div>
                                    <div class="w_tp_song_name">
                                        <h3><a href="#">${music.nameMusic}</a></h3>
                                        <p>${music.nameArtists}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ms_divider"></div> `;
                    $('.index-GetMusic_Top1115').append(html);
                }
            }
        }
    },
    error: function (error) {
        console.log("Loi");
    }
})

$.ajax({
    url: "/Home/Index_Featured_Artists",
    type: "GET",
    contentType: "application/json",
    beforeSend: function () {

    },
    success: function (res) {
        if (res && res.length > 0) {
            $('.get-Featured_Artists').html('');
            for (let i = 0; i < res.length; i++) {
                let artists = res[i];
                let html = `
                    <div class="swiper-slide" >
                    <div class="ms_rcnt_box" style="width:297px">
                        <div class="ms_rcnt_box_img">
                            <img src="/images/artist/${artists.imageArtists}" alt="">
                            <div class="ms_main_overlay">
                                <div class="ms_box_overlay"></div>
                                <div class="ms_play_icon">
                                    <img img src="/images/svg/play.svg" alt="">
                                </div>
                            </div>
                        </div>
                        <div class="ms_rcnt_box_text">
                            <h3><a href="#">${artists.nameArtists}</a></h3>
                        </div>
                        </div>
                    </div>
                `;
                $('.get-Featured_Artists').append(html);
            }
        }
    },
    error: function (error) {
        console.log("Loi");
    }

})

$.ajax({
    url: "/Home/Index_New_Releases",
    type: "GET",
    contentType: "application/json",
    beforeSend: function () {

    },
    success: function (res) {
        if (res && res.length > 0) {
            $('.get_New_Releases').html('');
            for (let i = 0; i < res.length; i++) {
                let new_Releases = res[i];
                let html = `
                        <div class="swiper-slide">
                        <div class="ms_release_box">
                            <div class="w_top_song">
                                <span class="slider_dot"></span>
                                <div class="w_tp_song_img">
                                <a href="/Home/Index?id=${new_Releases.idMusic}">
                                    <img src="/images/ImageMusic/${new_Releases.imageMusic}" alt="">
                                    <div class="ms_song_overlay">
                                    </div>
                                    <div class="ms_play_icon">
                                        <img src="/images/svg/play.svg" alt="">
                                    </div>
                                </a>
                                </div>
                                <div class="w_tp_song_name">
                                    <h3><a href="/Home/Index?id=${new_Releases.idMusic}">${new_Releases.nameMusic}</a></h3>
                                    <p>${new_Releases.nameArtists}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
                $('.get_New_Releases').append(html);
            }
        }
    },
    error: function (error) {
        console.log("Loi");
    }

})

