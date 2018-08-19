
    let baseUrl = "https://localhost:44342/api/";
    $(function () {
        $.ajax({
            url: baseUrl + "courses",
            method: "GET",
            success: data => console.log(data),
            error: err => console.log(err)
        });








        //TODO







    });
  
