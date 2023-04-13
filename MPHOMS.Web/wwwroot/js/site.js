$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/Hubs/signalOrder").build()

    connection.start()

    /*Refresh Function Insert DML*/
    connection.on("refreshOrdersInsert", function () {
        loadAlert();
        loadData();
        //console.log('Se ha actualizado una orden');
        LoadCommand(OrderId);
    })

    loadData();

    function loadAlert() {

        'use strict';

        var toasty = new Toasty({
            transition: "fade",
            duration: 0, // calculated automatically.
            enableSounds: true,
            progressBar: true,
            autoClose: true,
            onShow: function (type) {
                console.log("a toast " + type + " message is shown!");
            },
            onHide: function (type) {
                console.log("the toast " + type + " message is hidden!");
            }
        });

        toasty.configure({
            classname: "alert",
            insertBefore: false,
            progressBar: true,
            enableSounds: true
        });

        // register the new transition:
        toasty.transition("scale");

        // and run the first alert message:
        toasty.success("A new order have been placed.", 2500);

    }

    function loadData() {
        var tr = ''

        $.ajax({
            url: '/Home/GetOrders',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr = tr + `<tr>
                        <td>${v.orderId}</dv>
                        <td>${v.status}</td>
                        <td>${v.paymentStatus}</td>
                        <td>${v.email}</td>
                        <td>${v.store}</td>
                        <td class="text-primary">
                           <a href="#" type="button" rel="tooltip" title="View Order" class="btn btn-primary btn-link btn-sm" onclick="ShowOrder(${v.orderId})">
                              <i class="material-icons">assignment</i>
                           </a>
                           <a href="#" type="button" rel="tooltip" title="print" class="btn btn-primary btn-link btn-sm" onclick="LoadCommand(${v.orderId})"  >
                              <i class="material-icons">print</i>
                            </a>
                        </td>
                    </tr>`
                })

                $("#tableBody").html(tr)
            },
            error: (error) => {
                console.log(error)
            }
        })
    }

    /*Print Function from detail button with ESC/POS Commands for star TSP100 */
    var LoadCommand = function (OrderId) {
        $.ajax({
            url: '/Home/LoadCommands',
            data: { Id: OrderId },
            method: 'GET'
        }).done(function (data) {
            console.log('respuesta satisfactoria');
            print(data);
        });
    }
})