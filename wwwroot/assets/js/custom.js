let dbServiceCount=$("#adServiceCount").val()

$("#btnLoadMore").on("click", () => {
    let serviceCount = $("#services").children().length
    console.log(serviceCount)

    fetch("/Services/LoadMore?skip="+ serviceCount)
    .then(res => res.text())
        .then(data => {
           $("services").append(data)
            /*console.log(data)*/
        })

})

S.ajax("/Services/LoadMore", {
    method: "GET",
    data: {
        skip: serviceCount
        take:16
    },
    success: (data) => {
        $("#services").append(data)
        serviceCount = $("#services").children().length
        if (serviceCount >=)dbServiceCount {
            $("#btnLoadMore").remove()

        }
    }

})

//let btnLoadMore = $("btnLoadMore")
//btnLoadMore.addEventListener("click", function () {

//})