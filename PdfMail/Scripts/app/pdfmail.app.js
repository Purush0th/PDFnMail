;
(function ($, jsPdf, toast, progress) {

	function generatePdf(message) {
		var doc = new jsPdf();
		doc.text("jsPDF to Mail", 40, 30);
		doc.text(message, 40, 50);

		var binary = doc.output();
		return binary ? btoa(binary) : "";

	}

	$(document).ready(function () {
		$(document).on("submit", "#mail-pdf", function (e) {
			//prevent the form from doing a submit
			e.preventDefault();
			var formStatus = $("#mail-pdf")[0].checkValidity();
			if (!formStatus)
				return;
			var mailTo = $("#mail-to");
			var mailMessage = $("#mail-message");

			var pdfData = generatePdf(mailMessage.val());
			if (!pdfData)
				return;

			//Indicate progress
			progress.start();


			var reqData = {
				to: mailTo.val(),
				attachment: pdfData
			};

			$.ajax({
				url: "Home/Export",
				data: JSON.stringify(reqData),
				dataType: "json",
				type: "POST",
				contentType: "application/json; charset=utf-8",
				success: function (response) {
					progress.done();

					if (response.result === "success") {

						mailMessage.val("");
						mailTo.val("");

						toast.success("Mail send succesfully!");
					}
					else
						toast.error("There seems to be a problem, Please try again!");

				},
				error: function () {
					progress.done();
					toast.error("Error in communicating with server!");
				}
			});
		});
	});
})(jQuery, jsPDF, toastr, NProgress);