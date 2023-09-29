function changeEditCard(xId) {
	var count = document.getElementById(`${xId}`).value;
	let countConver = parseInt(count);
	if (countConver <= 0) {
		countConver = 1;
	}
	let obj = {
		IdAddCart: xId,
		CountAddCart: countConver,
		PriceProductShop: $('#PriceProductShop').val(),
	}
	$.ajax({
		url: "/Shopping/Shopping/EditCartAdd",
		type: "POST",
		contentType: "application/json",
		data: JSON.stringify(obj),
		beforeSend: function () {
			console.log("Before");
			console.log(obj);
		},
		success: function (res) {
			if (res > 0) {
				window.location.href = '/Shopping/Shopping/Cart';
			} else {
				alert("Update thất bại...");
			}
		},
		error: function (error) {
			console.log("Lỗi");
			console.log(error);
			alert("Update mới thất bại...");
		}
	})

}


function UpdateCardUser(xId, tangGiam) {
	var count = document.getElementById(`${xId}`).value;
	let countConver = parseInt(count);
	let countEdit = countConver + tangGiam;
	if (countEdit > 0) {
		console.log(xId, countConver, countEdit)
		let obj = {
			IdAddCart: xId,
			CountAddCart: countEdit,
			PriceProductShop: $('#PriceProductShop').val(),
		}
		$.ajax({
			url: "/Shopping/Shopping/EditCartAdd",
			type: "POST",
			contentType: "application/json",
			data: JSON.stringify(obj),
			beforeSend: function () {
				console.log("Before");
				console.log(obj);
			},
			success: function (res) {
				if (res > 0) {
					window.location.href = '/Shopping/Shopping/Cart';
					//$("#loadCard").load("addCart.js")
				} else {
					alert("Update thất bại...");
				}
			},
			error: function (error) {
				console.log("Lỗi");
				console.log(error);
				alert("Update mới thất bại...");
			}
		})
	}
}